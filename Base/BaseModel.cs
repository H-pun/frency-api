using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Frency.DataAccess.Extensions;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Frency.Base
{
    public abstract class BaseModel
    {
        private string[] _includedProperty;
        public virtual TEntity MapToEntity<TEntity>() where TEntity : BaseEntity
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap(GetType(), typeof(TEntity))).CreateMapper();
            TEntity entity = (TEntity)mapper.Map(this, GetType(), typeof(TEntity));
            if (entity.File != null) entity.GetType().GetProperty("FilePath").SetValue(entity, entity.Id.ToString());
            return entity;
        }

        public virtual void MapToModel<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap(typeof(TEntity), GetType())).CreateMapper();
            mapper.Map(entity, this);
        }

        public virtual List<TEntity> MaptoListEntity<TEntity>() where TEntity : BaseEntity { throw new NotImplementedException(); }

        public void IncludeProperty(string[] properties)
        {
            _includedProperty = properties;
        }

        public string[] GetIncludedProperty()
        {
            return _includedProperty;
        }
    }
}
