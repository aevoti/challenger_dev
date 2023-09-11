import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarTopicosComponent } from './listar-topicos.component';

describe('ListarTopicosComponent', () => {
  let component: ListarTopicosComponent;
  let fixture: ComponentFixture<ListarTopicosComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ListarTopicosComponent]
    });
    fixture = TestBed.createComponent(ListarTopicosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
