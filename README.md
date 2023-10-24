# AplicacionDeIncidenciasRH
      ## Falta implementar sistema de notificaciones/ Autogeneracion de reportes mensuales

# Gestión de Informes:

GET /api/reports: Obtener una lista de todos los informes presentados.
GET /api/reports/{id}: Obtener detalles de un informe específico por su identificador.
POST /api/reports: Presentar un nuevo informe de incidencia.
PUT /api/reports/{id}: Actualizar los detalles de un informe existente (por ejemplo, estado de resolución).
DELETE /api/reports/{id}: Eliminar un informe de incidencia.
# Gestión de Quejas:

GET /api/complaints: Obtener una lista de todas las quejas registradas.
GET /api/complaints/{id}: Obtener detalles de una queja específica por su identificador.
POST /api/complaints: Registrar una nueva queja.
PUT /api/complaints/{id}: Actualizar los detalles de una queja existente.
DELETE /api/complaints/{id}: Eliminar una queja registrada.
# Solicitudes de Permisos:

GET /api/leave-requests: Obtener una lista de todas las solicitudes de permisos presentadas.
GET /api/leave-requests/{id}: Obtener detalles de una solicitud de permiso específica por su identificador.
POST /api/leave-requests: Presentar una nueva solicitud de permiso.
PUT /api/leave-requests/{id}: Actualizar los detalles de una solicitud de permiso existente (por ejemplo, estado de aprobación).
DELETE /api/leave-requests/{id}: Cancelar una solicitud de permiso.
# Solicitudes de Vacaciones:

GET /api/vacation-requests: Obtener una lista de todas las solicitudes de vacaciones presentadas.
GET /api/vacation-requests/{id}: Obtener detalles de una solicitud de vacaciones específica por su identificador.
POST /api/vacation-requests: Presentar una nueva solicitud de vacaciones.
PUT /api/vacation-requests/{id}: Actualizar los detalles de una solicitud de vacaciones existente (por ejemplo, estado de aprobación).
DELETE /api/vacation-requests/{id}: Cancelar una solicitud de vacaciones.
# Estadísticas:

GET /api/statistics/incidents: Obtener estadísticas generales de incidencias, como el número de informes, quejas resueltas, etc.
GET /api/statistics/leave-requests: Obtener estadísticas relacionadas con las solicitudes de permisos, como el tiempo promedio de aprobación.
GET /api/statistics/vacation-requests: Obtener estadísticas relacionadas con las solicitudes de vacaciones, como la distribución por departamento.
