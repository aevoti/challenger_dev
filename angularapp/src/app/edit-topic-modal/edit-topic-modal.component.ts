import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Topic } from '../models/topic.model';

@Component({
  selector: 'app-edit-topic-modal',
  templateUrl: './edit-topic-modal.component.html',
  styleUrls: ['./edit-topic-modal.component.css']
})
export class EditTopicModalComponent {
  @Input() isEditTopicModalOpen: boolean = false;
  @Input() selectedTopic: any;
  @Output() updateTopic: EventEmitter<Topic> = new EventEmitter<Topic>();
  @Output() closeModal: EventEmitter<void> = new EventEmitter<void>();

  editedTopic: Topic = { id: 0, title: '', content: '', comments: [] }; // Inicialize com um objeto vazio

  ngOnChanges() {
    // Define editedTopic com base no tópico selecionado
    this.editedTopic = this.selectedTopic ? { ...this.selectedTopic } : null;
  }

  close() {
    // Emitir o evento para fechar o modal
    this.closeModal.emit();
  }

  // Método para atualizar o tópico
  update() {
    if (this.editedTopic) {
      this.updateTopic.emit(this.editedTopic);
    }
  }

}
