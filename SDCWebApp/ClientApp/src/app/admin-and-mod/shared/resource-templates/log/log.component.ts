import { Component, OnInit, Input } from '@angular/core';
import { ActivityLog } from 'src/app/models/ActivityLog';

@Component({
    selector: 'app-log',
    templateUrl: './log.component.html',
    styleUrls: ['./log.component.scss']
})
export class LogComponent implements OnInit {
    @Input() public log: ActivityLog;

    constructor() { }

    ngOnInit() { }
}
