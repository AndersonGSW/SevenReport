import { Component, Inject, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DxReportViewerComponent } from 'devexpress-reporting-angular';

@Component({
  selector: 'report-viewer',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './report-viewer.html',
  styleUrls: [
    '../../../node_modules/jquery-ui/themes/base/all.css',
    '../../../node_modules/devextreme/dist/css/dx.common.css',
    '../../../node_modules/devextreme/dist/css/dx.light.css',
    '../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.common.css',
    '../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.light.css',
    '../../../node_modules/devexpress-reporting/dist/css/dx-webdocumentviewer.css'
  ]
})
export class ReportViewerComponent {
  @ViewChild(DxReportViewerComponent, { static: false }) viewer: DxReportViewerComponent;
  reportUrl = '';
  reportUri = '';
  invokeAction = '/DXXRDV';
 
    constructor(@Inject('BASE_URL') public hostUrl: string, private _route: ActivatedRoute) { }

  // tslint:disable-next-line: use-lifecycle-interface
  ngOnInit() {
    this.reportUri = this._route.snapshot.paramMap.get('reportUri');
    this.reportUrl = '?reportName=' + this.reportUri;
    // this.viewer.bindingSender.OpenReport("TestReport?parameter1=40");
  }
}
