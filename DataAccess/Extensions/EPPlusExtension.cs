using OfficeOpenXml;

namespace Frency.DataAccess.Extensions
{
    public static class EPPlusExtension
    {
        public static List<T> ConvertSheetToObjects<T>(this ExcelWorksheet worksheet)
        {
            //Get the properties of T
            var tprops = Activator.CreateInstance<T>()
                .GetType()
                .GetProperties()
                .ToList();

            //Cells only contains references to cells with actual data
            var groups = worksheet.Cells
                .GroupBy(cell => cell.Start.Row)
                .ToList();

            //Assume the second row represents column data types (big assumption!)
            var types = groups
                .Skip(1)
                .First()
                .Select(rcell => rcell.Value.GetType())
                .ToList();

            //Assume first row has the column names
            var colNames = groups
                .First()
                .Select((hcell, idx) => new { Name = hcell.Value.ToString(), index = idx })
                .Where(o => tprops.Select(p => p.Name).Contains(o.Name))
                .ToList();

            //Everything after the header is data
            var rowValues = groups
                .Skip(1) //Exclude header
                .Select(cg => cg.Select(c => c.Value).ToList());

            //Create the collection container
            List<T> collection = rowValues
                .Select(row =>
                {
                    var tnew = Activator.CreateInstance<T>();
                    colNames.ForEach(colname =>
                    {
                        //This is the real wrinkle to using reflection - Excel stores all numbers as double including int
                        var val = row[colname.index];
                        var type = types[colname.index];
                        var prop = tprops.First(p => p.Name == colname.Name);

                        //If it is numeric it is a double since that is how excel stores all numbers
                        if (type == typeof(double))
                        {
                            //Unbox it
                            var unboxedVal = (double)val;

                            //FAR FROM A COMPLETE LIST!!!
                            if (prop.PropertyType == typeof(Int32))
                                prop.SetValue(tnew, (int)unboxedVal);
                            else if (prop.PropertyType == typeof(double))
                                prop.SetValue(tnew, unboxedVal);
                            else if (prop.PropertyType == typeof(DateTime))
                                prop.SetValue(tnew, DateTime.FromOADate(unboxedVal));
                            else
                                throw new NotImplementedException($"Type '{prop.PropertyType.Name}' not implemented yet!");
                        }
                        else
                        {
                            //Its a string
                            prop.SetValue(tnew, val);
                        }
                    });

                    return tnew;
                }).Cast<T>().ToList();

            //Send it back
            return collection;
        }
    }
}
