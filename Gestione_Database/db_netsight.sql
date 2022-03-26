-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Mar 26, 2022 alle 11:52
-- Versione del server: 10.4.6-MariaDB
-- Versione PHP: 7.3.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_netsight`
--

-- --------------------------------------------------------

--
-- Struttura della tabella `collegati`
--

CREATE TABLE `collegati` (
  `CodUtente` int(10) UNSIGNED NOT NULL,
  `CodPC` int(10) UNSIGNED NOT NULL,
  `orarioCollegamento` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32;

-- --------------------------------------------------------

--
-- Struttura della tabella `edifici`
--

CREATE TABLE `edifici` (
  `Cod` int(10) UNSIGNED NOT NULL,
  `Nome` varchar(40) NOT NULL,
  `Indirizzo` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32;

--
-- Dump dei dati per la tabella `edifici`
--

INSERT INTO `edifici` (`Cod`, `Nome`, `Indirizzo`) VALUES
(2, 'Lotto arancione', 'Via Santa Caterina da Siena 3, Mariano Comense '),
(3, 'Lotto giallo', 'Via Santa Caterina da Siena 3, Mariano Comense '),
(1, 'Lotto rosso', 'Via Santa Caterina da Siena 3, Mariano Comense ');

-- --------------------------------------------------------

--
-- Struttura della tabella `laboratori`
--

CREATE TABLE `laboratori` (
  `Cod` int(10) UNSIGNED NOT NULL,
  `Nome` varchar(40) NOT NULL,
  `CodEdificio` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32;

--
-- Dump dei dati per la tabella `laboratori`
--

INSERT INTO `laboratori` (`Cod`, `Nome`, `CodEdificio`) VALUES
(1, 'lab1', 2),
(2, 'lab2', 1),
(3, 'lab3', 3),
(4, 'lab3', 2),
(5, 'lab4', 1);

-- --------------------------------------------------------

--
-- Struttura della tabella `pc`
--

CREATE TABLE `pc` (
  `Cod` int(10) UNSIGNED NOT NULL,
  `Nome` varchar(40) NOT NULL,
  `IndirizzoIP` varchar(15) NOT NULL,
  `Stato` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'Se = 0 -> Spento',
  `CodLaboratorio` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32;

--
-- Dump dei dati per la tabella `pc`
--

INSERT INTO `pc` (`Cod`, `Nome`, `IndirizzoIP`, `Stato`, `CodLaboratorio`) VALUES
(1, 'pc1', '172.16.102.51', 0, 1),
(2, 'pc2', '172.16.102.52', 0, 1),
(3, 'pc3', '172.16.102.53', 0, 2),
(4, 'pc4', '172.16.102.54', 0, 3),
(5, 'pc5', '172.16.102.55', 0, 2);

-- --------------------------------------------------------

--
-- Struttura della tabella `utenti`
--

CREATE TABLE `utenti` (
  `Cod` int(10) UNSIGNED NOT NULL,
  `Nome` varchar(40) NOT NULL,
  `Cognome` varchar(40) NOT NULL,
  `DataNascita` date NOT NULL,
  `Email` varchar(40) NOT NULL,
  `Password` char(32) NOT NULL,
  `Admin` tinyint(1) NOT NULL DEFAULT 0 COMMENT 'se = 0 -> utente normale'
) ENGINE=InnoDB DEFAULT CHARSET=utf32;

--
-- Dump dei dati per la tabella `utenti`
--

INSERT INTO `utenti` (`Cod`, `Nome`, `Cognome`, `DataNascita`, `Email`, `Password`, `Admin`) VALUES
(1, 'Daniele', 'Oricchio', '2003-01-13', 'danieleoricchio@gmail.com', '1a1dc91c907325c69271ddf0c944bc72', 0),
(2, 'Mattia', 'D\'Ippolito', '2003-05-22', 'mattiadippolito@gmail.com', '1a1dc91c907325c69271ddf0c944bc72', 0),
(3, 'Lorenzo', 'Raia', '2003-08-14', 'lorenzoraia@gmail.com', '1a1dc91c907325c69271ddf0c944bc72', 0),
(4, 'Thomas', 'Cazzola', '2003-03-14', 'thomascazzola@gmail.com', '1a1dc91c907325c69271ddf0c944bc72', 0),
(5, 'Stefano', 'Potenza', '2003-05-01', 'stefanopotenza@gmail.com', '1a1dc91c907325c69271ddf0c944bc72', 0),
(6, 'Paolo', 'Terraneo', '2003-01-08', 'paoloterraneo@gmail.com', '1a1dc91c907325c69271ddf0c944bc72', 0),
(8, 'admin', 'admin', '2003-03-01', 'admin@gmail.com', '1a1dc91c907325c69271ddf0c944bc72', 1);

--
-- Indici per le tabelle scaricate
--

--
-- Indici per le tabelle `collegati`
--
ALTER TABLE `collegati`
  ADD PRIMARY KEY (`CodUtente`,`CodPC`),
  ADD KEY `acceso` (`CodPC`);

--
-- Indici per le tabelle `edifici`
--
ALTER TABLE `edifici`
  ADD PRIMARY KEY (`Cod`),
  ADD UNIQUE KEY `Nome` (`Nome`,`Indirizzo`);

--
-- Indici per le tabelle `laboratori`
--
ALTER TABLE `laboratori`
  ADD PRIMARY KEY (`Cod`),
  ADD KEY `appartiene` (`CodEdificio`);

--
-- Indici per le tabelle `pc`
--
ALTER TABLE `pc`
  ADD PRIMARY KEY (`Cod`),
  ADD KEY `siTrova` (`CodLaboratorio`);

--
-- Indici per le tabelle `utenti`
--
ALTER TABLE `utenti`
  ADD PRIMARY KEY (`Cod`),
  ADD UNIQUE KEY `Email` (`Email`) USING BTREE;

--
-- AUTO_INCREMENT per le tabelle scaricate
--

--
-- AUTO_INCREMENT per la tabella `edifici`
--
ALTER TABLE `edifici`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT per la tabella `laboratori`
--
ALTER TABLE `laboratori`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT per la tabella `pc`
--
ALTER TABLE `pc`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT per la tabella `utenti`
--
ALTER TABLE `utenti`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Limiti per le tabelle scaricate
--

--
-- Limiti per la tabella `collegati`
--
ALTER TABLE `collegati`
  ADD CONSTRAINT `acceso` FOREIGN KEY (`CodPC`) REFERENCES `pc` (`Cod`) ON DELETE CASCADE,
  ADD CONSTRAINT `collegato` FOREIGN KEY (`CodUtente`) REFERENCES `utenti` (`Cod`) ON DELETE CASCADE;

--
-- Limiti per la tabella `laboratori`
--
ALTER TABLE `laboratori`
  ADD CONSTRAINT `appartiene` FOREIGN KEY (`CodEdificio`) REFERENCES `edifici` (`Cod`) ON DELETE CASCADE;

--
-- Limiti per la tabella `pc`
--
ALTER TABLE `pc`
  ADD CONSTRAINT `siTrova` FOREIGN KEY (`CodLaboratorio`) REFERENCES `laboratori` (`Cod`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

