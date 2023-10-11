import { Component, OnInit } from '@angular/core';
import { TopicService } from '../../services/topic.service';
import { Topic } from '../../models/topic.model';
import { Comment } from '../../models/comment.model'
import { Router } from '@angular/router';

@Component({
  selector: 'app-topics',
  templateUrl: './topics.component.html',
  styleUrls: ['./topics.component.css']
})
export class TopicsComponent implements OnInit {
  topics: Topic[] = []
  selectedTopic: any
  selectedTopicForComment: any
  isCreateTopicModalOpen: boolean = false;
  isEditTopicModalOpen: boolean = false;
  isCreateCommentModalOpen: boolean = false;
  createTopic: Topic = {  } as Topic;
  newComment: Comment = { usersId: 0, content: '' };

  constructor(private router: Router, private topicService: TopicService) { }

  ngOnInit(): void {
    this.topicService.getTopics().subscribe(topics => {
      this.topics = topics; // Armazene a lista de tópicos na propriedade do componente
    });
  }

  onTopicClick(topic: Topic) {
    this.selectedTopic = topic;

    this.topicService.getTopicById(topic.id).subscribe(topic => {

      // Verifique se selectedTopic não é nulo antes de atribuir os comentários
      if (this.selectedTopic) {
        console.log('Selected Topic:', this.selectedTopic);
        this.selectedTopic.comments = topic.comments;
      }
    });
  }

  openCreateTopicModal() {
    this.isCreateTopicModalOpen = true;
  }

  openEditTopicModal(topic: Topic) {
    this.selectedTopic = topic; // Define o tópico selecionado
    this.isEditTopicModalOpen = true;
  }

  closeCreateTopicModal() {
    this.isCreateTopicModalOpen = false;
  }

  onTopicCreate(newTopic: Topic) {
    this.topicService.createTopic(newTopic).subscribe(createdTopic => {

      if (this.selectedTopic) {
        this.selectedTopic.comments = createdTopic.comments;
      }

      this.closeCreateTopicModal();
    });
  }

  createComment() {
    this.topicService.createComment(this.selectedTopic.id, this.newComment.content).subscribe(() => {
      //fechar modal ou atualizar
    });
  }

  openCreateCommentModal() {
    //this.selectedTopic = newComment; // Define o tópico selecionado
    this.isEditTopicModalOpen = true;
  }

  deleteTopic(topicId: number) {
    this.topicService.deleteTopic(topicId).subscribe(() => {
      this.refreshTopics();
    });
  }

  refreshTopics() {
    this.topicService.getTopics().subscribe((topics) => {
      this.topics = topics;
    });
  }


  onTopicUpdate(updatedTopic: Topic) {
    this.topicService.updateTopic(updatedTopic).subscribe(() => {
      this.refreshTopics();
      this.closeEditTopicModal();
    });
  }

  closeEditTopicModal() {
    this.isEditTopicModalOpen = false;
    this.selectedTopic = null;
  }

  deleteComment(topicId: number, commentId: number) {
    this.topicService.deleteComment(topicId, commentId).subscribe(() => {
      // Após a exclusão bem-sucedida, recarregue a página
      window.location.reload();
    });
  }

  closeCreateCommentModal() {
    this.isCreateCommentModalOpen = false;
  }
}
