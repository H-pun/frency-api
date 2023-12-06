<?php
// Mendapatkan data yang dikirim melalui POST
// $data = $_POST['fileList'];

// Nama folder tujuan penyimpanan file
$model = $_POST['modelName'];

$data = [
    make_script($model, null, 'Entity'),
    make_script($model, null, 'Service'),
    make_script($model, null, 'Controller'),
    make_script($model, 'Create', 'Request'),
    // make_script($model, 'Delete', 'Request'),
    make_script($model, 'Detail', 'Response'),
    // make_script($model, 'List', 'Response'),
    make_script($model, 'Update', 'Request'),
];

// Looping setiap elemen array
foreach ($data as $item) {
    // Mendapatkan nama file dan path dari elemen array
    $fileName = $item['fileName'];
    $fileContent = $item['fileContext'];
    $directory = realpath(__DIR__);

    if (str_replace($model, "", $fileName) == "Controller.cs") {
        $destination =  realpath($directory . '/../../../') . '/Controllers/' . $fileName;
    } else if (str_replace($model, "", $fileName) == "Service.cs") {
        $destination =  realpath($directory . '/../../../') . '/DataAccess/Services/' . $fileName;
    } else if (str_replace($model, "", $fileName) == "Entity.cs") {
        $fileName = str_replace("Entity", "", $fileName);
        $destination =  realpath($directory . '/../../../') . '/DataAccess/Entities/' . $fileName;
    } else {
        // Menggabungkan path tujuan dengan nama file
        $destination =  realpath($directory . '/../../../') . '/DataAccess/Models/' . $model . '/' . $fileName;

        // Membuat folder jika belum ada
        $folderPath = dirname($destination);
        if (!is_dir($folderPath)) {
            mkdir($folderPath);
        }
    }

    // Menyimpan file ke folder tujuan
    file_put_contents($destination, $fileContent);
}

// Menampilkan pesan jika penyimpanan berhasil
echo json_encode([
    'message' => 'File berhasil disimpan.',
    'data' => $data
], JSON_PRETTY_PRINT);

function make_script($replacement, $prefix, $suffix)
{
    $fileContent = file_get_contents(__DIR__ . '/template/' . $prefix . $suffix . '.txt');
    return [
        'fileName' => $prefix . $replacement . $suffix . '.cs',
        'fileContext' => str_replace("CustomName", $replacement, $fileContent)
    ];
}
