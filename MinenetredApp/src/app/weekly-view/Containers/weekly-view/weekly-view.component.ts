import { Component, OnInit } from '@angular/core';
import { IProject } from './../../../Interfaces/ProjectInterface';
import {  ProjectsService } from './../../../Services/ProjectsService/projects.service';

@Component({
  selector: 'app-weekly-view',
  templateUrl: './weekly-view.component.html',
  styleUrls: ['./weekly-view.component.scss']
})
export class WeeklyViewComponent implements OnInit {

  constructor(private projectService: ProjectsService) { }
  response : IProject[];
  formatedDates : Array<String>;
  dates : Array<Date>;

  ngOnInit() {
    this.projectService.GetOpenProjects().subscribe(
      r => { 
        this.response = r ;
        }
    );
  }
  private GetDayOfTheWeek(date : Date) : string{
    let days = ['Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday'];
    return days[date.getDay()];
  }
  private GetStringMonth(date : Date) : string{
    let months = ['01','02','03','04','05','06','07','08','09','10','11','12'];
    return months[date.getMonth()];
  }
  
  BuildDatesArray(date:string){
    this.dates = new Array<Date>();
    let FormatedDate = new Date(
      Number.parseInt(date.substring(0,4)),
      Number.parseInt(date.substring(5,7))-1 ,
      Number.parseInt(date.substring(8))      
    );
    for (let index = 0; index < 5; index++) {
      let dateToAdd = new Date(FormatedDate);
      dateToAdd.setDate(dateToAdd.getDate()+index);
      this.dates.push(dateToAdd);      
    }
    this.BuildDatesFormated();
  }

  private BuildDatesFormated(){
    this.formatedDates = new Array<String>();
    this.dates.forEach(element => {
      let toAdd = "";
      toAdd += this.GetDayOfTheWeek(element) + " ";
      toAdd += element.getFullYear() + "-";
      toAdd += this.GetStringMonth(element) + "-";
      toAdd += element.getDate()< 10 ? "0" + element.getDate() : element.getDate();
      this.formatedDates.push(toAdd);
    });
  }

}
