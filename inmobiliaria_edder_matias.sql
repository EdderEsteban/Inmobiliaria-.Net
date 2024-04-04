-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 02-04-2024 a las 08:00:20
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria_edder_matias`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `id_contrato` int(11) NOT NULL,
  `id_inquilino` int(11) DEFAULT NULL,
  `id_inmueble` int(11) DEFAULT NULL,
  `fecha_inicio` date DEFAULT NULL,
  `fecha_fin` date DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `id_inmueble` int(11) NOT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `uso` enum('comercial','residencial') DEFAULT NULL,
  `id_tipo` int(11) DEFAULT NULL,
  `cantidad_ambientes` int(11) DEFAULT NULL,
  `precio_alquiler` decimal(10,2) DEFAULT NULL,
  `latitud` varchar(255) DEFAULT NULL,
  `longitud` varchar(255) DEFAULT NULL,
  `id_propietario` int(11) DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `id_inquilino` int(11) NOT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(255) DEFAULT NULL,
  `dni` int(8) NOT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `correo` varchar(255) DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`id_inquilino`, `nombre`, `apellido`, `dni`, `direccion`, `telefono`, `correo`, `id_usuario`, `fecha`) VALUES
(1, 'Edder', 'Santibañez', 93962239, 'San Luis', '2664271316', 'edder@santy.com', NULL, '2024-04-02 00:00:00'),
(2, 'Diego', 'Orellano', 98765432, 'Santa Fe', '2665122345', 'diego@orellano.com', NULL, '2024-04-02 00:00:00'),
(6, 'Mauricio', 'Ferrieres', 45685212, 'San Luis', '2664854565', 'mauricio@ferrieres.com', NULL, '2024-04-02 00:00:00'),
(8, 'Fatima', 'Lebri', 29426271, 'San Luis', '2664718225', 'fatima@lebri.com', NULL, '2024-04-02 00:00:00'),
(10, 'Jose', 'Perez', 44333222, 'Concaran', '2664568923', 'jose@perez.com', NULL, '2024-04-02 00:00:00');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `id_pago` int(11) NOT NULL,
  `id_contrato` int(11) DEFAULT NULL,
  `fecha_pago` date DEFAULT NULL,
  `monto` decimal(10,2) DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `id_propietario` int(11) NOT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(255) DEFAULT NULL,
  `dni` int(8) NOT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `telefono` varchar(20) DEFAULT NULL,
  `correo` varchar(255) DEFAULT NULL,
  `id_usuario` int(11) DEFAULT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`id_propietario`, `nombre`, `apellido`, `dni`, `direccion`, `telefono`, `correo`, `id_usuario`, `fecha`) VALUES
(1, 'Matias', 'Diaz', 12345678, 'Merlo', '2665122345', 'maty@diaz.com', NULL, '2024-04-02 00:02:25');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_inmueble`
--

CREATE TABLE `tipo_inmueble` (
  `id_tipo` int(11) NOT NULL,
  `tipo` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `id_usuario` int(11) NOT NULL,
  `nombre` varchar(255) NOT NULL,
  `apellido` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `rol` enum('administrador','empleado') NOT NULL,
  `fecha` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`id_contrato`),
  ADD KEY `id_inquilino` (`id_inquilino`),
  ADD KEY `id_inmueble` (`id_inmueble`),
  ADD KEY `fk_contrato_usuario` (`id_usuario`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`id_inmueble`),
  ADD KEY `id_propietario` (`id_propietario`),
  ADD KEY `fk_inmueble_usuario` (`id_usuario`),
  ADD KEY `fk_inmueble_tipo` (`id_tipo`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`id_inquilino`),
  ADD KEY `fk_inquilino_usuario` (`id_usuario`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`id_pago`),
  ADD KEY `id_contrato` (`id_contrato`),
  ADD KEY `fk_pago_usuario` (`id_usuario`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`id_propietario`),
  ADD KEY `fk_propietario_usuario` (`id_usuario`);

--
-- Indices de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  ADD PRIMARY KEY (`id_tipo`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id_usuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `id_contrato` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `id_inmueble` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `id_inquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `id_pago` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `id_propietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  MODIFY `id_tipo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id_usuario` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`id_inquilino`) REFERENCES `inquilino` (`id_inquilino`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`id_inmueble`) REFERENCES `inmueble` (`id_inmueble`),
  ADD CONSTRAINT `fk_contrato_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `fk_inmueble_tipo` FOREIGN KEY (`id_tipo`) REFERENCES `tipo_inmueble` (`id_tipo`),
  ADD CONSTRAINT `fk_inmueble_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`),
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`id_propietario`) REFERENCES `propietario` (`id_propietario`);

--
-- Filtros para la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD CONSTRAINT `fk_inquilino_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `fk_pago_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`),
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`id_contrato`) REFERENCES `contrato` (`id_contrato`);

--
-- Filtros para la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD CONSTRAINT `fk_propietario_usuario` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
