<?php
    require 'classes/json_message.php';
    header("Content-type: application/json;charset=utf-8");
    $link= mysqli_connect("localhost","root","","db_netsight");
    if($link === false){
        die();
    }
    $nomi = array();
    if ($_SERVER['REQUEST_METHOD']!="GET") die(json_encode(new JsonMessage(400, "Utilizzare metodo GET", false,  null)));
    $sql = "SELECT edifici.Nome, edifici.Cod FROM edifici ORDER BY Cod;";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        foreach ($all as $value) {
            $pair = array("nome"=>$value[0], "cod"=>$value[1]);
            array_push($nomi,$pair);
        }
        echo json_encode(new JsonMessage(200, "Richiesta accettata", true, $nomi));
    }
    mysqli_close($link);
    die();
?>