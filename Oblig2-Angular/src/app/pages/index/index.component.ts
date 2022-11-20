import { Component, OnInit } from '@angular/core';
import { PageoptionsService } from '../../services/pageoptions.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  providers: [PageoptionsService]
})
export class IndexComponent implements OnInit {

  // This component is only here to provide a unique instance
  // of pageoptionsservice to its children

  constructor() {
  }

  ngOnInit(): void { }

}
