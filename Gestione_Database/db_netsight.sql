-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Mar 26, 2022 alle 11:28
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

-- --------------------------------------------------------

--
-- Struttura della tabella `laboratori`
--

CREATE TABLE `laboratori` (
  `Cod` int(10) UNSIGNED NOT NULL,
  `Nome` varchar(40) NOT NULL,
  `CodEdificio` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf32;

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
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `laboratori`
--
ALTER TABLE `laboratori`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `pc`
--
ALTER TABLE `pc`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `utenti`
--
ALTER TABLE `utenti`
  MODIFY `Cod` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

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
