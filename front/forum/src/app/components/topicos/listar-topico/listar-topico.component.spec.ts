import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarTopicoComponent } from './listar-topico.component';

describe('ListarTopicoComponent', () => {
  let component: ListarTopicoComponent;
  let fixture: ComponentFixture<ListarTopicoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ListarTopicoComponent]
    });
    fixture = TestBed.createComponent(ListarTopicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
