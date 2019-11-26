import { Component, OnInit, Input } from '@angular/core';
import { IProject } from './../../../Interfaces/ProjectInterface';
import {TimeEntrtyService} from '../../../Services/TimeEntryService/time-entrty.service';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-weekly-table',
  templateUrl: './weekly-table.component.html',
  styleUrls: ['./weekly-table.component.scss']
})
export class WeeklyTableComponent implements OnInit {

  constructor(private timeEntryService : TimeEntrtyService) { }

  @Input() projectList: IProject[]; 
  @Input() tableHeaders: Array<string>; 

  ngOnInit() {
  }
  ngOnChanges(){
    if(this.tableHeaders){
      this.AddHoursToProjects();
    }
  }
  private AddHoursToProjects(){
    this.projectList.forEach(project => {
      project.hoursPerday = new Array<number>();
      this.tableHeaders.forEach((element, index) => {
        let startingIndex = element.length - 10;
        let formatedDate = element.substring(startingIndex);
        this.timeEntryService.GetHoursPerProjectAndDay(formatedDate, project.id).subscribe(
          h => {
            project.hoursPerday[index]=h;
          }
        );
      });
      console.log(project.hoursPerday);
    });

  }
  GetHours(){

  }

}
