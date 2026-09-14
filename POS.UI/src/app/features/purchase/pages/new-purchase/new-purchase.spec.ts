import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NewPurchase } from './new-purchase';

describe('NewPurchase', () => {
  let component: NewPurchase;
  let fixture: ComponentFixture<NewPurchase>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewPurchase],
    }).compileComponents();

    fixture = TestBed.createComponent(NewPurchase);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
