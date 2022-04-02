<?php

//controllo se l'utente è già loggato
session_start();
require_once 'config.php';
/*if (isset($_SESSION['user_id'])) {
    header('Location: menu.php');
}*/


//save datas from client
$email = $_POST['email'];
$password = $_POST['password'];
//$userIP = $_POST['ip'];
//$socket=socket_create(AF_INET, SOCK_STREAM, SOL_TCP);


if(!empty($email) && !empty($password)){
    $sql= 'SELECT * FROM utenti where email="'.$email.'" AND password="'.md5($password).'"';
    if($result = mysqli_query($link, $sql));
    else{
        echo "Errore!!!";
        //header("Location: index.html");
    }
    if(mysqli_num_rows($result)==1){
        $row = mysqli_fetch_array($result, MYSQLI_ASSOC);
        //Prendo il nome dell'utente
        $nome = GiveName($link, $email);
        
        $_SESSION['user_id'] = $nome;
        echo "successful";
        //SendPacket($userIP, $socket);
    }else{
        header('Location: index.html');
    }
}




function GiveName($link, $email){
    $sqlNome= 'SELECT nome FROM utenti where email="'.$email.'"';
    $resultNome = mysqli_query($link, $sqlNome);
    if($resultNome = mysqli_query($link, $sqlNome));
    if(mysqli_num_rows($resultNome)==1){
        $row = mysqli_fetch_array($resultNome, MYSQLI_ASSOC);
        $name = $row['nome'];
        return $name;
    }else{
        return "unnamed";
    }
}

//Funzione che crea una connessione TCP
/*function SendPacket($ip, $sock){
    if(!$sock){
        $errorcode = socket_last_error();
        $errormsg = socket_strerror($errorcode);
        die("Couldn't create socket: [$errorcode] $errormsg");
    }
    if(!socket_connect($sock, $ip, 123)){
        echo "Cannot send the packet. please retry";
    }
    else{
        echo "Connection succeded";
    }
}*/
?>