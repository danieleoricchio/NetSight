<?php
    require 'classes/json_message.php';
    header("Content-type: application/json;charset=utf-8");
    $link= mysqli_connect("localhost","root","","db_netsight");
    if($link === false){
        die();
    }
    $nomi = array();
    if ($_SERVER['REQUEST_METHOD']!="GET") die(json_encode(new JsonMessage(400, "Utilizzare metodo GET", false, null)));
    if (!isset($_GET['codedificio']) || empty($_GET['codedificio'])) die(json_encode(new JsonMessage(400, "Parametro 'codedificio' non inserito", false, null)));
    if (!isset($_GET['codadmin']) || empty($_GET['codadmin'])) die(json_encode(new JsonMessage(400, "Parametro 'codadmin' non inserito", false, null)));
    $codEdificio = $_GET['codedificio'];
    $codadmin = $_GET['codadmin'];
    $sql = "SELECT laboratori.Nome FROM laboratori JOIN edifici ON edifici.Cod = laboratori.CodEdificio JOIN gestione_laboratori ON gestione_laboratori.codLab = laboratori.Cod WHERE gestione_laboratori.codAdmin = $codadmin AND laboratori.CodEdificio = $codEdificio;";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        foreach ($all as $value) {
            array_push($nomi,$value[0]);
        }
        echo json_encode(new JsonMessage(200, "Richiesta accettata", true,$nomi));
    }
    mysqli_close($link);
    die();
?>