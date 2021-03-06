-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Mag 03, 2022 alle 16:46
-- Versione del server: 10.4.22-MariaDB
-- Versione PHP: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
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
-- Struttura della tabella `admin`
--

CREATE TABLE `admin` (
  `Cod` int(10) UNSIGNED NOT NULL,
  `Nome` varchar(40) NOT NULL,
  `Cognome` varchar(40) NOT NULL,
  `DataNascita` date NOT NULL,
  `Email` varchar(40) NOT NULL,
  `Password` char(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32;

--
-- Dump dei dati per la tabella `admin`
--

INSERT INTO `admin` (`Cod`, `Nome`, `Cognome`, `DataNascita`, `Email`, `Password`) VALUES
(1, 'Lorenzo', 'Raia', '2003-08-14', 'admin@gmail.com', '1a1dc91c907325c69271ddf0c944bc72'),
(2, 'Daniele', 'Oricchio', '2003-01-13', 'danieleoricchio@gmail.com', '1a1dc91c907325c69271ddf0c944bc72');

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
(14, 'Casa lori', 'Via'),
(2, 'Lotto arancione', 'Via Santa Caterina da Siena 3, Mariano Comense '),
(3, 'Lotto giallo', 'Via Santa Caterina da Siena 3, Mariano Comense '),
(1, 'Lotto rosso', 'Via Santa Caterina da Siena 3, Mariano Comense ');

-- --------------------------------------------------------

--
-- Struttura della tabella `gestione_laboratori`
--

CREATE TABLE `gestione_laboratori` (
  `codLab` int(10) UNSIGNED NOT NULL,
  `codAdmin` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32;

--
-- Dump dei dati per la tabella `gestione_laboratori`
--

INSERT INTO `gestione_laboratori` (`codLab`, `codAdmin`) VALUES
(1, 1),
(2, 1),
(3, 2),
(7, 1),
(9, 1),
(11, 1);

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
(7, 'labprova', 1),
(9, 'Lab casa lori', 14),
(11, 'Lab casa lori 2', 14);

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
(1, 'pc1', '87.5.55.26', 0, 1),
(2, 'pc2', '172.16.102.52', 0, 1),
(3, 'pc3', '172.16.102.53', 0, 2),
(4, 'pc4', '172.16.102.54', 0, 3),
(5, 'pc5', '172.16.102.55', 0, 2),
(6, 'pc5', '172.16.102.83', 0, 2);

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
  `Password` char(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32;

--
-- Dump dei dati per la tabella `utenti`
--

INSERT INTO `utenti` (`Cod`, `Nome`, `Cognome`, `DataNascita`, `Email`, `Password`) VALUES
(1, 'Daniele', 'Oricchio', '2003-01-13', 'danieleoricchio@gmail.com', '1a1dc91c907325c69271ddf0c944bc72'),
(2, 'Mattia', 'D\'Ippolito', '2003-05-22', 'mattiadippolito@gmail.com', '1a1dc91c907325c69271ddf0c944bc72'),
(3, 'Lorenzo', 'Raia', '2003-08-14', 'lorenzoraia@gmail.com', '1a1dc91c907325c69271ddf0c944bc72'),
(4, 'Thomas', 'Cazzola', '2003-03-14', 'thomascazzola@gmail.com', '1a1dc91c907325c69271ddf0c944bc72'),
(5, 'Stefano', 'Potenza', '2003-05-01', 'stefanopotenza@gmail.com', '1a1dc91c907325c69271ddf0c944bc72'),
(6, 'Paolo', 'Terraneo', '2003-01-08', 'paoloterraneo@gmail.com', '1a1dc91c907325c69271ddf0c944bc72');

--
-- Indici per le tabelle scaricate
--

--
-- Indici per le tabelle `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`Cod`),
  ADD UNIQUE KEY `Email` (`Email`) USING BTREE,
  ADD UNIQUE KEY `Email_2` (`Email`);

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
-- Indici per le tabelle `gestione_laboratori`
--
ALTER TABLE `gestione_laboratori`
  ADD KEY `vincoloAdmin` (`codAdmin`),
  ADD KEY `vincoloLab` (`codLab`);

--
-- Indici per le tabelle `laboratori`
--
ALTER TABLE `laboratori`
  ADD PRIMARY KEY (`Cod`),
  ADD UNIQUE KEY `Nome` (`Nome`),
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
  ADD UNIQUE KEY `Email` (`Email`) USING BTREE,
  ADD UNIQUE KEY `Email_2` (`Email`);

--
-- AUTO_INCREMENT per le tabelle scaricate
--

--
-- AUTO_INCREMENT per la tabella `admin`
--
ALTER TABLE `admin`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT per la tabella `edifici`
--
ALTER TABLE `edifici`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT per la tabella `laboratori`
--
ALTER TABLE `laboratori`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT per la tabella `pc`
--
ALTER TABLE `pc`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT per la tabella `utenti`
--
ALTER TABLE `utenti`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

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
-- Limiti per la tabella `gestione_laboratori`
--
ALTER TABLE `gestione_laboratori`
  ADD CONSTRAINT `vincoloAdmin` FOREIGN KEY (`codAdmin`) REFERENCES `admin` (`Cod`) ON DELETE CASCADE,
  ADD CONSTRAINT `vincoloLab` FOREIGN KEY (`codLab`) REFERENCES `laboratori` (`Cod`) ON DELETE CASCADE;

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
