<?php
    require 'classes/laboratorio.php';
    require 'classes/pc.php';

    header("Content-type: application/json;charset=utf-8");
    $link= mysqli_connect("localhost","root","","db_netsight");
    if($link === false){
        die();
    }
    $laboratori= array();
    $sql = "SELECT * FROM laboratori";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        foreach ($all as $value) {
            array_push($laboratori, new laboratorio($value[0],$value[1],$value[2]));
        }
        #echo json_encode($laboratori);
    }
    $sql = "SELECT pc.Nome, pc.IndirizzoIP, pc.Stato, laboratori.Cod FROM pc JOIN laboratori ON pc.CodLaboratorio = laboratori.Cod ORDER BY laboratori.Cod, pc.Cod";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        #var_dump($all);
        foreach ($all as $value) {
            InserisciPCInLab($laboratori,TrovaPosizioneLaboratorio($laboratori, $value[3]),new pc($value[0],$value[1],boolval($value[2])));
        }
        echo json_encode($laboratori);
    }
    mysqli_close($link);

    function InserisciPCInLab(array &$labs, int $poslab, pc $pc)
    {
        array_push($labs[$poslab]->listaPc, $pc);
    }
    function TrovaPosizioneLaboratorio(array &$laboratori, int $codiceLab){
        for ($i=0; $i < count($laboratori); $i++) { 
            if ($laboratori[$i]->cod == $codiceLab)
                return $i;
        }
        return null;
    }
?>