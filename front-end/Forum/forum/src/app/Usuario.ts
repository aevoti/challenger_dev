export interface Usuario {
    id: string;
    nome: string;
    passwordHash: string;
    email: string;
    foto: Uint8Array;
  }