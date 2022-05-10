<?php
require_once "config.php";
$email = $_GET['email'];
$codlab = $_GET['codLab'];
$query = "SELECT * FROM admin WHERE admin.email='$email'";
if ($result = mysqli_query($link, $query)) {
    if (mysqli_num_rows($result) > 0) {
        $row = mysqli_fetch_array($result);
        $codAdmin = $row['Cod'];
        $query = "INSERT INTO gestione_laboratori (codLab, codAdmin) VALUES ('$codlab','$codAdmin')";
        $result = mysqli_query($link, $query);
        if ($result) {
            $data = array("status" => "ok", "message" => "operazione eseguita con successo");
            header("Content-Type: application/json");
            echo json_encode($data);
            exit();
        }else{
            $data = array("status" => "error", "message" => "admin non trovato");
            header("Content-Type: application/json");
            echo json_encode($data);
            exit();
        }
    }
}
?>