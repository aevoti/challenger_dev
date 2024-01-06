import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal-confirmacao',
  templateUrl: './modal-confirmacao.component.html',
  styleUrls: ['./modal-confirmacao.component.css'],
})
export class ModalConfirmacaoComponent {
  id: number = 0;
  tipo: string = "";
  tipoAcao: string = "";

  constructor(public activeModal: NgbActiveModal) {}

  confirmarExclusao() {
    this.activeModal.close('confirmado');
  }

  cancelarExclusao() {
    this.activeModal.dismiss('cancelado');
  }
}
