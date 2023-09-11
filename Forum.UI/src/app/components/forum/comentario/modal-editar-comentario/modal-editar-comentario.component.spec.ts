import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEditarComentarioComponent } from './modal-editar-comentario.component';

describe('ModalEditarComentarioComponent', () => {
  let component: ModalEditarComentarioComponent;
  let fixture: ComponentFixture<ModalEditarComentarioComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModalEditarComentarioComponent]
    });
    fixture = TestBed.createComponent(ModalEditarComentarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
