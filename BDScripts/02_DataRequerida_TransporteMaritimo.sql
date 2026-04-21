USE [TransporteMaritimoDB]
GO

INSERT INTO Usuarios
(
    UsuarioId,
    Nombre,
    Email,
	PasswordHash,
	Activo,
	IntentosFallidos,
	BloqueadoHasta
)
VALUES
(
	1,
    'admin',
    'admin@flota.com',
    '$2a$11$pF7TagyQHrIz7v5eQwgT1O.wr3H61Od5X0MPzYhSi.mloZWCc99Eu',
	1,
	0,
	NULL
);


INSERT INTO Roles (RolId, NombreRol, Descripcion)
VALUES
(1, 'Administrador', NULL),
(2, 'Capitan', NULL),
(3, 'PrimerOficial', NULL),
(4, 'Ingeniero', NULL),
(5, 'PersonalBase', NULL),
(6, 'Marinero', NULL)
