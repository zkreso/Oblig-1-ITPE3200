import { Component, OnInit } from '@angular/core';
import { Observable, switchMap } from 'rxjs';
import { Disease } from '../../models';
import { DatabaseService } from '../../services/database.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-diseasedetails',
  templateUrl: './diseasedetails.component.html',
  styleUrls: ['./diseasedetails.component.css']
})
export class DiseasedetailsComponent implements OnInit {
  disease$!: Observable<Disease>;

  constructor(private ds: DatabaseService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.disease$ = this.route.paramMap.pipe(
      switchMap(params => this.ds.getDisease(Number(params.get('id'))))
    );
  }

}
