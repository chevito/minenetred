import { Component, OnInit, Output, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.scss']
})
export class DatePickerComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  
  date : string;

  @Output()
  outString : EventEmitter<string> = new EventEmitter<string>();

  changedDate(){
    this.outString.emit(this.date);
  }

}
