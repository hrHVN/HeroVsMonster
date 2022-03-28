using System;

namespace HeroVsMonster.Interface
{
	public interface ICharacter
	{
		string CharacterName { get; set; }
		string CharacterClass { get; set; 
		int CharacterExperience { get; set; }
		int CharacterHealt { get; set; }
		int CharcterLevel { get; set; }
	}
}
