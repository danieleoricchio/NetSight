<?php
    require 'classes/edificio.php';
    require 'classes/json_message.php';

    header("Content-type: application/json;charset=utf-8");
    $link= mysqli_connect("localhost","root","","db_netsight");
    if($link === false){
        die();
    }
    if ($_SERVER['REQUEST_METHOD']!="GET") die(json_encode(new JsonMessage(400, "Utilizzare metodo GET", false, null)));
    if (!isset($_GET['codedificio']) || empty($_GET['codedificio'])) die(json_encode(new JsonMessage(400, "Parametro 'codedificio' non inserito", false, null)));
    $codedificio = $_GET['codedificio'];
    $sql = "SELECT * FROM edifici WHERE Cod = $codedificio";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        foreach ($all as $value) {
            $edificio = new edificio(intval($value[0]),$value[1],$value[2]);
        }
        echo json_encode(new JsonMessage(200, "Richiesta accettata", true, $edificio));
    }
    mysqli_close($link);
?>