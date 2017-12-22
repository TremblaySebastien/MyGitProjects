-- phpMyAdmin SQL Dump
-- version 4.4.12
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Jeu 07 Décembre 2017 à 22:29
-- Version du serveur :  5.5.42
-- Version de PHP :  5.5.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `api`
--

-- --------------------------------------------------------

--
-- Structure de la table `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL,
  `username` text NOT NULL,
  `password` text NOT NULL,
  `email` text NOT NULL,
  `highscore` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=latin1;

--
-- Contenu de la table `user`
--

INSERT INTO `user` (`id`, `username`, `password`, `email`, `highscore`) VALUES
(31, 'seb', 'f15389363cf66828af65a1d8881e5821', 'seb@gmail.com', 0),
(32, 'bob', '9f9d51bc70ef21ca5c14f307980a29d8', 'Bob@Caramail.com', 10),
(33, 'gilles', 'e543fdb4737f66b96e764d7303a15ae8', 'gilles@gmail.com', 20),
(34, 'arthur', '68830aef4dbfad181162f9251a1da51b', 'arthur@gmail.com', 30),
(35, 'clement', '236e92bcf7c04d8d7ff3f798b537823f', 'clement@gmail.com', 40),
(36, 'cletus', '2c87a0128dc323bff723b1a5c86ac6c3', 'cletus@gmail.com', 50),
(37, 'theodore', '56a525aa777f9e85e239bae7a958b02c', 'theodore@gmail.com', 60),
(38, 'linda', 'eaf450085c15c3b880c66d0b78f2c041', 'linda@gmail.com', 80),
(39, 'frank', '26253c50741faa9c2e2b836773c69fe6', 'frank@gmail.com', 70),
(40, 'samuel', 'd8ae5776067290c4712fa454006c8ec6', 'samuel@gmail.com', 80),
(41, 'remy', '27152949302e3bd0d681a6f0548912b9', 'remy@gmail.com', 90),
(43, 'manu', 'f13bb1bed03db9d68a7d9a48aafeec78', 'manu@gmail.com', 0),
(44, 'christo', '5dd67f520ff9b0e695bea5fe7e8cf3c6', 'christo@gmail.com', 0);

--
-- Index pour les tables exportées
--

--
-- Index pour la table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT pour les tables exportées
--

--
-- AUTO_INCREMENT pour la table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=45;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
