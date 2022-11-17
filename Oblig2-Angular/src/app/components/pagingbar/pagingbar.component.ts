import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-pagingbar',
  templateUrl: './pagingbar.component.html',
  styleUrls: ['./pagingbar.component.css']
})
export class PagingbarComponent implements OnInit {
  @Input() vm: any;
  @Output() clickPrevPage: EventEmitter<void> = new EventEmitter;
  @Output() clickNextPage: EventEmitter<void> = new EventEmitter;
  @Output() clickPageNum: EventEmitter<number> = new EventEmitter;
  @Output() clickPageSize: EventEmitter<number> = new EventEmitter;

  pageSizeControl = new FormControl("10");

  constructor() { }

  ngOnInit(): void {
  }

  prevPage(): void {
    this.clickPrevPage.emit();
  }

  nextPage(): void {
    this.clickNextPage.emit();
  }

  goToPage(n: number): void {
    if (n == NaN || n == undefined || n == null) {
      return;
    }
    this.clickPageNum.emit(n);
  }

  changePageSize(): void {
    let n = parseInt(this.pageSizeControl.value!);
    if (n == NaN || n == undefined || n == null) {
      return;
    }
    this.clickPageSize.emit(n);
  }

  // helper function

  arr(n: number): number[] {
    let indices = [];
    for (let i = 0; i < n; i++) {
      indices.push(i + 1);
    }
    return indices;
  }

}
