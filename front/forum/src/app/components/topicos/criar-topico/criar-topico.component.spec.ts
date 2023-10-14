import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CriarTopicoComponent } from './criar-topico.component';

describe('CriarTopicoComponent', () => {
  let component: CriarTopicoComponent;
  let fixture: ComponentFixture<CriarTopicoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CriarTopicoComponent]
    });
    fixture = TestBed.createComponent(CriarTopicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
