import { SessionData } from '../model/auth/SessionData';

export default class SessionService {

  static LOCAL_SESSION: SessionData | null = this.getLocalSession();

  static getLocalSession(): SessionData | null {
    const stringSettings = localStorage.getItem('session');
    if (!stringSettings) return null;
    return JSON.parse(stringSettings);
  }

  static saveLocalSession(data: SessionData | null) {
    localStorage.setItem('session', JSON.stringify(data));
    this.LOCAL_SESSION = data;
  }

  static destroyLocalSession() {
    localStorage.removeItem('session');
    localStorage.clear();
    sessionStorage.clear();
    location.replace("/login?logout=true");
    this.LOCAL_SESSION = null;
  }

}