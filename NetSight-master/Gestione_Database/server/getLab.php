<?php
    require 'classes/laboratorio.php';
    require 'classes/pc.php';
    require 'classes/json_message.php';

    header("Content-type: application/json;charset=utf-8");
    $link= mysqli_connect("localhost","root","","db_netsight");
    if($link === false){
        die();
    }

    if ($_SERVER['REQUEST_METHOD']!="GET") die(json_encode(new JsonMessage(400, "Utilizzare metodo GET", false)));
    if (!isset($_GET['name']) || empty($_GET['name'])) die(json_encode(new JsonMessage(400, "Parametro 'name' non inserito", false)));
    $nome = $_GET['name'];
    $sql = "SELECT * FROM laboratori WHERE laboratori.Nome = '$nome' LIMIT 1;";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        foreach ($all as $value) {
            #array_push($laboratori, new laboratorio($value[0],$value[1],$value[2]));
            $lab = new laboratorio($value[0],$value[1],$value[2]);
        }
        if (count($all)==0){
            die(json_encode(new JsonMessage(204, "Impossibile trovare laboratorio $nome", false)));
        }
        #echo json_encode($laboratori);
    }
    $sql = "SELECT Nome, IndirizzoIP, Stato, Cod FROM pc WHERE pc.CodLaboratorio = $lab->cod ORDER BY pc.Cod";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        #var_dump($all);
        foreach ($all as $value) {
            array_push($lab->listaPc,new pc($value[0],$value[1],boolval($value[2]),$value[3]));
        }
        echo json_encode($lab);
    }
    mysqli_close($link);