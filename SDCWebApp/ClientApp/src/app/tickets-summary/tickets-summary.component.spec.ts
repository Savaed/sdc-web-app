import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketsSummaryComponent } from './tickets-summary.component';

describe('TicketsSummaryComponent', () => {
  let component: TicketsSummaryComponent;
  let fixture: ComponentFixture<TicketsSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketsSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketsSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
