<?php
    require 'classes/json_message.php';
    header("Content-type: application/json;charset=utf-8");
    $link= mysqli_connect("localhost","root","","db_netsight");
    if($link === false){
        die();
    }
    $nomi = array();
    if ($_SERVER['REQUEST_METHOD']!="GET") die(json_encode(new JsonMessage(400, "Utilizzare metodo GET", false)));
    if (!isset($_GET['codadmin']) || empty($_GET['codadmin'])) die(json_encode(new JsonMessage(400, "Parametro 'codadmin' non inserito", false)));
    $codadmin = $_GET['codadmin'];
    $sql = "SELECT DISTINCT edifici.Nome FROM edifici JOIN laboratori ON laboratori.CodEdificio = edifici.Cod JOIN gestione_laboratori ON laboratori.Cod = gestione_laboratori.codLab WHERE gestione_laboratori.codAdmin = $codadmin;";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        foreach ($all as $value) {
            array_push($nomi,$value[0]);
        }
        echo json_encode($nomi);
    }
    mysqli_close($link);
    die();
?>