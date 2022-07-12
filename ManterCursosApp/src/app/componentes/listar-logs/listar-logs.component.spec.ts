import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListarLogsComponent } from './listar-logs.component';

describe('ListarLogsComponent', () => {
  let component: ListarLogsComponent;
  let fixture: ComponentFixture<ListarLogsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListarLogsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListarLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
