import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Comment } from '../models/comment.model';

@Component({
  selector: 'app-create-comment-modal',
  templateUrl: './create-comment-modal.component.html',
  styleUrls: ['./create-comment-modal.component.css']
})
export class CreateCommentModalComponent {
  @Input() isCreateCommentModalOpen: boolean = false;
  @Output() createCommentEvent: EventEmitter<Comment> = new EventEmitter<Comment>();
  @Output() closeModalEvent: EventEmitter<void> = new EventEmitter<void>();

  newComment: Comment = { usersId: 0, content: '' };

  close() {
    this.closeModalEvent.emit();
  }

  createComment() {
    // Emitir o evento para criar o comentário
    this.createCommentEvent.emit(this.newComment);
  }
}
