import { customElement } from "aurelia-framework";

@customElement("hello-world")
export class HelloWorldViewModel {
    public message: string = Date.now().toString();
}