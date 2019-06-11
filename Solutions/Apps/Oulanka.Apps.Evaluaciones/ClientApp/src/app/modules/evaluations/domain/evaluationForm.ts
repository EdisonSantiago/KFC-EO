export class EvaluationForm {

    Id: number;
    TipoEvaluacion: any;
    OwnerId: number;
    NombreRGM: string;
    NombreMIC: string;
    Posicion: number;
    TipoVisita: number;
    ParteDia: number;
    IdLocal: any;

    constructor(
        id: number,
        tipoEvaluacion: any,
        ownerId: number,
        nombreRGM: string,
        nombreMIC: string,
        posicion: number,
        tipoVisita: number,
        parteDia: number,
        idLocal: any
    ) {
        this.Id = id;
        this.TipoEvaluacion = tipoEvaluacion;
        this.OwnerId = ownerId;
        this.NombreRGM = nombreRGM;
        this.NombreMIC = nombreMIC;
        this.Posicion = posicion;
        this.TipoVisita = tipoVisita;
        this.ParteDia = parteDia;
        this.IdLocal = idLocal;
    }

}
