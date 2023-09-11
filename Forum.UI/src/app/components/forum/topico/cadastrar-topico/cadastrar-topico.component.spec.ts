import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastrarTopicoComponent } from './cadastrar-topico.component';

describe('CadastrarTopicoComponent', () => {
  let component: CadastrarTopicoComponent;
  let fixture: ComponentFixture<CadastrarTopicoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CadastrarTopicoComponent]
    });
    fixture = TestBed.createComponent(CadastrarTopicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
