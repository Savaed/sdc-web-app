import { Component, OnInit, Input } from '@angular/core';
import { SightseeingGroup } from 'src/app/models/SightseeingGroup';

@Component({
    selector: 'app-visit-group',
    templateUrl: './visit-group.component.html',
    styleUrls: ['./visit-group.component.scss']
})
export class VisitGroupComponent implements OnInit {
    @Input() public group: SightseeingGroup;

    constructor() { }

    ngOnInit() { }
}
