import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeeklyViewComponent } from './Containers/weekly-view/weekly-view.component';
import { WeeklyTableComponent } from './Components/weekly-table/weekly-table.component';
import { DatePickerComponent} from './Components/date-picker/date-picker.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
      WeeklyTableComponent,
      WeeklyViewComponent,
      DatePickerComponent
    ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports:[
    WeeklyViewComponent
  ]
})
export class WeeklyViewModule { }
