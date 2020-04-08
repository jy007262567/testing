import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { person, pet } from '../common.types';
import { tap } from 'rxjs/internal/operators';


@Component({
  selector: 'app-json',
  templateUrl: './json.component.html'
})
export class JsonComponent implements OnInit {

  peopleM: person[];
  peopleF: person[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {

    let people: person[];
    let resultM = [];
    let resultF = [];
    this.http.get('http://agl-developer-test.azurewebsites.net/people.json')
      .pipe
      (
        tap(num => console.log(num))
      )
      .subscribe(data => {
        people = (<person[]>data).map(res => res);
        people.forEach(element => {
          if (element.pets != null) {
            if (element.gender == "Male") {
              element.pets.forEach(element => {
                if (element.type == "Cat") {
                  resultM.push(element);
                }
              })

            } else {
              element.pets.forEach(element => {
                if (element.type == "Cat") {
                  resultF.push(element);
                }
              })
            }
          }
        });
        resultM = resultM.sort(
          (x: pet, y: pet) => {
            var a = x.name.toLowerCase();
            var b = y.name.toLowerCase();
            if (a < b) return -1;
            if (a > b) return 1;
            return 0;
          }
        )
        resultF = resultF.sort(
          (x: pet, y: pet) => {
            var a = x.name.toLowerCase();
            var b = y.name.toLowerCase();
            if (a < b) return -1;
            if (a > b) return 1;
            return 0;
          }
        )
        this.peopleM = resultM;
        this.peopleF = resultF;

      }

      );

  }

}
