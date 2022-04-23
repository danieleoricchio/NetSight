<?php
    header("Content-type: application/json;charset=utf-8");
    $link= mysqli_connect("localhost","root","","db_netsight");
    if($link === false){
        die();
    }
    $nomi = array();
    $sql = "SELECT Nome FROM laboratori";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        foreach ($all as $value) {
            array_push($nomi,$value[0]);
        }
        echo json_encode($nomi);
        #echo json_encode($laboratori);
    }
    mysqli_close($link);
    die();
?>