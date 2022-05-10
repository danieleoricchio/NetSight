<?php
require_once "config.php";
/* if ($_SERVER["REQUEST_METHOD"] == "POST") { */
/* $email = $_POST["email"];
$codLab= $_POST["laboratorio"]; */
$email = "nuovo@gmail.com";
$nome = "nuovo";
$cognome = "nuovoCogn";
$dataNascita = "2001-02-02";
$password = "ciao";
$codlab = "1";
$query = "SELECT * FROM admin WHERE email='$email'";
if ($result = mysqli_query($link, $query)) {
    if (mysqli_num_rows($result) == 1) {
        $query = " SELECT laboratori.nome,admin.Cod FROM admin,gestione_laboratori,laboratori WHERE admin.email='admin@gmail.com'AND admin.cod=gestione_laboratori.codadmin AND gestione_laboratori.codlab=laboratori.cod";
        if ($result = mysqli_query($link, $query)) {
            echo "ciao";
            if (mysqli_num_rows($result) > 0) {
                echo "admin già esistente. Laboratori associati ad esso:<br>";
                while ($row = mysqli_fetch_array($result)) {
                    echo $row['nome'] . "<br>";
                }
            }
        }
    } else {
        $query = "INSERT INTO admin (nome,cognome,dataNascita, email, password) VALUES ('$nome','$cognome','$dataNascita','$email',MD5('$password'))";
        if ($result = mysqli_query($link, $query)) {
            $query = "SELECT * FROM admin WHERE admin.email='$email'";
            if ($result = mysqli_query($link, $query)) {
                if (mysqli_num_rows($result) > 0) {
                    echo "ciao";
                    $row = mysqli_fetch_array($result);
                    $codAdmin = $row['Cod'];
                    $query = "INSERT INTO gestione_laboratori (codLab, codAdmin) VALUES ('$codlab','$codAdmin')";
                    $result = mysqli_query($link, $query);
                    if ($result) {
                        echo ("operazione riuscita");
                    }
                }
            }
        }
    }
}
//}
