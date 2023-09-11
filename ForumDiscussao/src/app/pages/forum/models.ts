export class topicos{
    public id: number= 0;
    public data: string = "";
    public dsc: string =  "";
    public idUsu: number = 0;
    public idUsuNavigation: number | undefined;
    public comentario: Array<comentario> =  []
  }

  export class comentario{
    public id: number= 0;
    public idTopico: number= 0;
    public data: string = "";
    public dsc: string =  "";
    public IdTopicoNavigation: number = 0;
    public idUsuNavigation: number | undefined;
  }

  export class ordenacao{
    public name: string= "";
    public code: number = 0;
  }