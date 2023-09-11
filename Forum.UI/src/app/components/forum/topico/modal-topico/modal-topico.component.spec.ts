import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalTopicoComponent } from './modal-topico.component';

describe('ModalTopicoComponent', () => {
  let component: ModalTopicoComponent;
  let fixture: ComponentFixture<ModalTopicoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModalTopicoComponent]
    });
    fixture = TestBed.createComponent(ModalTopicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
