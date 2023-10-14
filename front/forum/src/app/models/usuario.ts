export class Usuario {
  id: number;
  nome: string;
  email: string;
  foto: string;

  constructor(id: number, nome: string, email: string, foto: string){
      this.id = id;
      this.nome = nome;
      this.email = email;
      this.foto = foto;
  }
}
