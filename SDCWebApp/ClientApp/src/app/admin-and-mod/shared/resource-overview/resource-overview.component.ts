import { Component, OnInit, Input } from '@angular/core';


@Component({
    selector: 'app-resource-overview',
    templateUrl: './resource-overview.component.html',
    styleUrls: ['./resource-overview.component.scss']
})
export class ResourceOverviewComponent implements OnInit {
    @Input() public id = '';
    @Input() public mainInfo: any = '';
    @Input() public secondaryInfo: any = '';
    @Input() public modifyDate: Date = new Date();
    @Input() public editable = false;

    constructor() { }

    ngOnInit() { }
}
