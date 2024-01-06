import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEditarTopicoComponent } from './modal-editar-topico.component';

describe('ModalEditarTopicoComponent', () => {
  let component: ModalEditarTopicoComponent;
  let fixture: ComponentFixture<ModalEditarTopicoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModalEditarTopicoComponent]
    });
    fixture = TestBed.createComponent(ModalEditarTopicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
