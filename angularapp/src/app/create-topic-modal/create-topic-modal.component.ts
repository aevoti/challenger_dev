import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Topic } from '../models/topic.model';

@Component({
  selector: 'app-create-topic-modal',
  templateUrl: './create-topic-modal.component.html',
  styleUrls: ['./create-topic-modal.component.css']
})
export class CreateTopicModalComponent {
  @Input() isCreateTopicModalOpen: boolean = false;
  @Output() create: EventEmitter<Topic> = new EventEmitter<Topic>();
  @Output() closeModal: EventEmitter<void> = new EventEmitter<void>();

  newTopic: Topic = { id: 0, title: '', content: '' };

  emitCreateEvent() {
  this.create.emit(this.newTopic);
}

  close() {
    // Emitir o evento para fechar o modal
    this.closeModal.emit();
  }

}
