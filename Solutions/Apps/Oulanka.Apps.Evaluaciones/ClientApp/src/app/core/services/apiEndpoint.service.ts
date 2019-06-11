import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ApiEndpointService {
    constructor() { }

    // Cadenas
    public cadenas(): string { return 'api/cadenas/'; }
    public cadenasList(): string { return this.cadenas() + 'list'; }

    // Catalogo
    public catalogo(): string { return 'api/catalogo/'; }
    public posicionesByCadena(): string { return 'posiciones/'; }
    public tiposVisita(): string { return 'tiposVisita/'; }
    public partesDia(): string { return 'partesdia/'; }

    // Evaluaciones
    public evaluaciones(): string { return 'api/evaluaciones/'; }
    public tipoEvaluaciones(): string { return 'tipos'; }
    public evaluacionesCreate(): string {return this.evaluaciones() + 'create'; }
    public evaluacionesGet(): string {return this.evaluaciones() + 'get'; }

    // Locales
    public locales(): string { return 'api/locales/'; }
    public localesByCadenaList(): string { return 'api/locales/list/'; }
    public localById(): string { return 'api/locales/get/'; }
    public localByCode(): string { return 'api/locales/getbycode/'; }

    // Estandares
    public estandares(): string {return 'api/estandares/'; }
    public estandarById(): string {return this.estandares() + 'getbyid/'; }
    public estandaresByGrupo(): string { return this.estandares() + 'getbygroup/'; }
    public estandaresByParent(): string { return this.estandares() + 'getbyparent/'; }
    public grupoEstandaresList(): string { return this.estandares() + 'grupos/'; }
    public picklistByParent(): string { return this.estandares() + 'picklist/'; }

    // Equipos
    public equipos(): string { return 'api/equipos/'; }
    public equiposTree(): string { return this.equipos() + 'tree'; }

    // imagenes
    public imagenes(): string { return 'api/imagenes/'; }
    public imagenesByEvaluacion(): string { return this.imagenes() + 'evaluacion/'; }
    public imagenesByLocal(): string { return this.imagenes() + 'local/'; }
    public imagenesByLocalSave(): string { return this.imagenes() + 'localsave/'; }

    // Respuestas
    public respuestas(): string { return 'api/respuestas/'; }
    public respuestasSave(): string { return this.respuestas() + 'save'; }
    public respuestasByEvaluacion(): string { return this.respuestas() + 'getbyeval/'; }
    public respuestasComentariosByEvaluacion(): string { return this.respuestas() + 'getcomentariosbyeval/'; }
}

