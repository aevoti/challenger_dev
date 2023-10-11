import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedServiceService {

  private selectedValueSource = new BehaviorSubject<string>('');
  selectedValue$ = this.selectedValueSource.asObservable();

  setSelectedValue(value: string) {
    this.selectedValueSource.next(value);
  }
  constructor() { }
}
