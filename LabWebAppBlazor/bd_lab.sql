CREATE TABLE usuario (
  id_usuario INT IDENTITY(1,1) PRIMARY KEY,
  correo_usuario VARCHAR(100) UNIQUE NOT NULL,
  clave_usuario VARCHAR(255) NOT NULL, -- Aquí guardamos el hash de la contraseña (temporal o permanente)
  rol VARCHAR(20) CHECK (rol IN ('administrador', 'recepcionista', 'laboratorista')) NOT NULL,
  nombre NVARCHAR(255) NOT NULL,
  es_contraseña_temporal BIT DEFAULT 1, -- Indica si la contraseña es temporal
  estado_registro BIT DEFAULT 0 -- Indica si el usuario ha cambiado la contraseña temporal
);

CREATE TABLE medico (
  id_medico INT IDENTITY(1,1) PRIMARY KEY,
  nombre_medico NVARCHAR(100) NOT NULL,
  especialidad NVARCHAR(100) NOT NULL,
  telefono VARCHAR(20),
  correo VARCHAR(100) UNIQUE,
  anulado BIT DEFAULT 0,
  id_usuario INT FOREIGN KEY REFERENCES usuario(id_usuario) ON DELETE SET NULL
);

CREATE TABLE paciente (
  id_paciente INT IDENTITY(1,1) PRIMARY KEY,
  cedula_paciente VARCHAR(20) UNIQUE NOT NULL,
  nombre_paciente NVARCHAR(100) NOT NULL,
  fecha_nac_paciente DATE NOT NULL,
  edad_paciente INT,
  direccion_paciente NVARCHAR(150),
  correo_electronico_paciente VARCHAR(100),
  telefono_paciente VARCHAR(20),
  fecha_registro DATETIME DEFAULT GETDATE(),
  anulado BIT DEFAULT 0,
  id_usuario INT FOREIGN KEY REFERENCES usuario(id_usuario) ON DELETE SET NULL
);

CREATE TABLE orden (
  id_orden INT IDENTITY(1,1) PRIMARY KEY,
  numero_orden NVARCHAR(50) UNIQUE NOT NULL,
  id_paciente INT FOREIGN KEY REFERENCES paciente(id_paciente) ON DELETE CASCADE,
  fecha_orden DATE NOT NULL,
  total DECIMAL(10, 2) NOT NULL,
  saldo_pendiente DECIMAL(10, 2) DEFAULT 0,
  total_pagado DECIMAL(10, 2) DEFAULT 0,
  estado_pago NVARCHAR(20) CHECK (estado_pago IN ('PENDIENTE', 'PAGADO', 'CANCELADO')) NOT NULL,
  anulado BIT DEFAULT 0,
  liquidado_convenio BIT DEFAULT 0,
  id_medico INT FOREIGN KEY REFERENCES medico(id_medico) ON DELETE SET NULL,
  observacion NVARCHAR(255),
  id_usuario INT FOREIGN KEY REFERENCES usuario(id_usuario) ON DELETE SET NULL
);

CREATE TABLE convenio (
  id_convenio INT IDENTITY(1,1) PRIMARY KEY,
  id_medico INT FOREIGN KEY REFERENCES medico(id_medico) ON DELETE CASCADE,
  fecha_convenio DATE NOT NULL,
  porcentaje_comision DECIMAL(5, 2) NOT NULL,
  monto_total DECIMAL(10, 2) NOT NULL,
  anulado BIT DEFAULT 0,
  id_usuario INT FOREIGN KEY REFERENCES usuario(id_usuario) ON DELETE SET NULL
);

CREATE TABLE examen (
  id_examen INT IDENTITY(1,1) PRIMARY KEY,
  nombre_examen NVARCHAR(100) NOT NULL,
  valor_referencia NVARCHAR(100),
  unidad NVARCHAR(50),
  precio DECIMAL(10, 2),
  anulado BIT DEFAULT 0,
  id_usuario INT FOREIGN KEY REFERENCES usuario(id_usuario) ON DELETE SET NULL
);

CREATE TABLE reactivo (
  id_reactivo INT IDENTITY(1,1) PRIMARY KEY,
  nombre_reactivo NVARCHAR(100) NOT NULL,
  fabricante NVARCHAR(100),
  unidad NVARCHAR(50),
  anulado BIT DEFAULT 0,
  cantidad_disponible DECIMAL(10, 2) DEFAULT 0
);

CREATE TABLE detalle_convenio (
  id_detalle_convenio INT IDENTITY(1,1) PRIMARY KEY,
  id_convenio INT FOREIGN KEY REFERENCES convenio(id_convenio) ON DELETE CASCADE,
  subtotal DECIMAL(10, 2) NOT NULL
);

CREATE TABLE pago (
  id_pago INT IDENTITY(1,1) PRIMARY KEY,
  id_orden INT FOREIGN KEY REFERENCES orden(id_orden) ON DELETE CASCADE,
  fecha_pago DATETIME DEFAULT GETDATE(),
  monto_pagado DECIMAL(10, 2),
  observacion NVARCHAR(255),
  anulado BIT DEFAULT 0,
  id_usuario INT FOREIGN KEY REFERENCES usuario(id_usuario) ON DELETE SET NULL
);

CREATE TABLE resultado (
    id_resultado INT IDENTITY(1,1) PRIMARY KEY,
    numero_resultado NVARCHAR(50) NOT NULL,
    id_paciente INT FOREIGN KEY REFERENCES paciente(id_paciente) ON DELETE NO ACTION,  
    id_medico INT FOREIGN KEY REFERENCES medico(id_medico) ON DELETE NO ACTION,  
    fecha_resultado DATETIME NOT NULL,
    observaciones NVARCHAR(255),
    id_orden INT FOREIGN KEY REFERENCES orden(id_orden) ON DELETE NO ACTION,  
    anulado BIT DEFAULT 0
);

CREATE TABLE detalle_pago (
  id_detalle_pago INT IDENTITY(1,1) PRIMARY KEY,
  id_pago INT FOREIGN KEY REFERENCES pago(id_pago) ON DELETE CASCADE,
  tipo_pago NVARCHAR(50),
  monto DECIMAL(10, 2),
  id_usuario INT FOREIGN KEY REFERENCES usuario(id_usuario) ON DELETE SET NULL,
  fecha_anulacion DATETIME
);

CREATE TABLE movimiento_reactivo (
  id_movimiento_reactivo INT IDENTITY(1,1) PRIMARY KEY,
  id_reactivo INT FOREIGN KEY REFERENCES reactivo(id_reactivo) ON DELETE CASCADE,
  tipo_movimiento NVARCHAR(50), -- Tipo: 'ingreso', 'salida'
  cantidad DECIMAL(10, 2),
  fecha_movimiento DATETIME NOT NULL,
  id_orden INT FOREIGN KEY REFERENCES orden(id_orden) ON DELETE CASCADE,
  observacion NVARCHAR(255)
);

CREATE TABLE examen_reactivo (
  id_examen_reactivo INT IDENTITY(1,1) PRIMARY KEY,
  id_examen INT FOREIGN KEY REFERENCES examen(id_examen) ON DELETE CASCADE,
  id_reactivo INT FOREIGN KEY REFERENCES reactivo(id_reactivo) ON DELETE CASCADE,
  cantidad_usada DECIMAL(10, 2),
  unidad NVARCHAR(50),
  id_usuario INT FOREIGN KEY REFERENCES usuario(id_usuario) ON DELETE SET NULL
);

CREATE TABLE detalle_orden (
  id_detalle_orden INT IDENTITY(1,1) PRIMARY KEY,
  id_orden INT FOREIGN KEY REFERENCES orden(id_orden) ON DELETE CASCADE,
  id_examen INT FOREIGN KEY REFERENCES examen(id_examen) ON DELETE CASCADE,
  precio DECIMAL(10, 2),
  id_resultado INT FOREIGN KEY REFERENCES resultado(id_resultado) ON DELETE CASCADE
);

CREATE INDEX idx_usuario_correo ON usuario(correo_usuario);
CREATE INDEX idx_orden_estado_pago ON orden(estado_pago);
CREATE INDEX idx_paciente_nombre ON paciente(nombre_paciente);
CREATE INDEX idx_pago_fecha ON pago(fecha_pago);

