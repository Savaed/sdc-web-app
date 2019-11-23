import { Component, OnInit, Input } from '@angular/core';
import { VisitInfo } from 'src/app/models/VisitInfo';

@Component({
    selector: 'app-visit-info',
    templateUrl: './visit-info.component.html',
    styleUrls: ['./visit-info.component.scss']
})
export class VisitInfoComponent implements OnInit {
    @Input() public info: VisitInfo;

    constructor() { }

    ngOnInit() { }
}
