export class Equipos {
  id: string;
  parentId:string;
  codigo: string;
  nombre: string;
  descripcion: string;
  notasEspeciales: string;
  tipoEstandar: number;
  estandarPadreId: string | null;
  estandarPadreNombre: string;
  creadoPor: string;
  actualizadoPor: string;
  creadoEn: Date | string;
  actualizadoEn: Date | string;
  estadoId: string;
  estadoNombre: string;
  grupoEstandarId: string;
  grupoEstandarNombre: string;
  nivelId: string;
  nivelNombre: string;
  categoriaId: string;
  categoriaNombre: string;
  clasificacionId: string;
  clasificacionNombre: string;
  hasChildren: boolean;
}
