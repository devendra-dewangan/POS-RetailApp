import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PurachaseDetails } from './purachase-details';

describe('PurachaseDetails', () => {
  let component: PurachaseDetails;
  let fixture: ComponentFixture<PurachaseDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PurachaseDetails],
    }).compileComponents();

    fixture = TestBed.createComponent(PurachaseDetails);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
